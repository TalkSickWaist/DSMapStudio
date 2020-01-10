﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Numerics;
using SoulsFormats;
using ImGuiNET;

namespace StudioCore.MsbEditor
{
    public class PropertyEditor
    {
        public ActionManager ContextActionManager;

        private Dictionary<string, PropertyInfo[]> PropCache = new Dictionary<string, PropertyInfo[]>();

        private object ChangingObject = null;
        private object ChangingPropery = null;
        private object ChangedValue = null;
        private Action LastUncommittedAction = null;

        public PropertyEditor(ActionManager manager)
        {
            ContextActionManager = manager;
        }

        private bool PropertyRow(Type typ, object oldval, out object newval)
        {
            if (typ == typeof(long))
            {
                long val = (long)oldval;
                string strval = $@"{val}";
                if (ImGui.InputText("##value", ref strval, 40))
                {
                    var res = long.TryParse(strval, out val);
                    if (res)
                    {
                        newval = val;
                        return true;
                    }
                }
            }
            else if (typ == typeof(int))
            {
                int val = (int)oldval;
                if (ImGui.InputInt("##value", ref val))
                {
                    newval = val;
                    return true;
                }
            }
            else if (typ == typeof(uint))
            {
                uint val = (uint)oldval;
                string strval = $@"{val}";
                if (ImGui.InputText("##value", ref strval, 16))
                {
                    var res = uint.TryParse(strval, out val);
                    if (res)
                    {
                        newval = val;
                        return true;
                    }
                }
            }
            else if (typ == typeof(short))
            {
                int val = (short)oldval;
                if (ImGui.InputInt("##value", ref val))
                {
                    newval = (short)val;
                    return true;
                }
            }
            else if (typ == typeof(ushort))
            {
                ushort val = (ushort)oldval;
                string strval = $@"{val}";
                if (ImGui.InputText("##value", ref strval, 5))
                {
                    var res = ushort.TryParse(strval, out val);
                    if (res)
                    {
                        newval = val;
                        return true;
                    }
                }
            }
            else if (typ == typeof(sbyte))
            {
                int val = (sbyte)oldval;
                if (ImGui.InputInt("##value", ref val))
                {
                    newval = (sbyte)val;
                    return true;
                }
            }
            else if (typ == typeof(byte))
            {
                byte val = (byte)oldval;
                string strval = $@"{val}";
                if (ImGui.InputText("##value", ref strval, 3))
                {
                    var res = byte.TryParse(strval, out val);
                    if (res)
                    {
                        newval = val;
                        return true;
                    }
                }
            }
            else if (typ == typeof(bool))
            {
                bool val = (bool)oldval;
                if (ImGui.Checkbox("##value", ref val))
                {
                    newval = val;
                    return true;
                }
            }
            else if (typ == typeof(float))
            {
                float val = (float)oldval;
                if (ImGui.DragFloat("##value", ref val, 0.1f))
                {
                    newval = val;
                    return true;
                    //shouldUpdateVisual = true;
                }
            }
            else if (typ == typeof(string))
            {
                string val = (string)oldval;
                if (val == null)
                {
                    val = "";
                }
                if (ImGui.InputText("##value", ref val, 40))
                {
                    newval = val;
                    return true;
                }
            }
            else if (typ == typeof(Vector3))
            {
                Vector3 val = (Vector3)oldval;
                if (ImGui.DragFloat3("##value", ref val, 0.1f))
                {
                    newval = val;
                    return true;
                    //shouldUpdateVisual = true;
                }
            }
            else
            {
                ImGui.Text("ImplementMe");
            }
            newval = null;
            return false;
        }

        private void ChangeProperty(object prop, MapObject selection, object obj, object newval,
            bool changed, bool committed, bool shouldUpdateVisual, int arrayindex = -1)
        {
            if (changed)
            {
                if (prop == ChangingPropery && selection.MsbObject == ChangingObject && LastUncommittedAction != null)
                {
                    if (ContextActionManager.PeekUndoAction() == LastUncommittedAction)
                    {
                        ContextActionManager.UndoAction();
                    }
                    else
                    {
                        LastUncommittedAction = null;
                    }
                }
                else
                {
                    LastUncommittedAction = null;
                }

                PropertiesChangedAction action;
                if (arrayindex != -1)
                {
                    action = new PropertiesChangedAction((PropertyInfo)prop, arrayindex, obj, newval);
                }
                else
                {
                    action = new PropertiesChangedAction((PropertyInfo)prop, obj, newval);
                }
                if (shouldUpdateVisual)
                {
                    action.SetPostExecutionAction((undo) =>
                    {
                        selection.UpdateRenderModel();
                    });
                }
                ContextActionManager.ExecuteAction(action);

                LastUncommittedAction = action;
                ChangingPropery = prop;
                ChangingObject = selection.MsbObject;
            }
            if (committed)
            {
                // Invalidate name cache
                selection.Name = null;

                // Undo and redo the last action with a rendering update
                if (LastUncommittedAction != null && ContextActionManager.PeekUndoAction() == LastUncommittedAction)
                {
                    if (LastUncommittedAction is PropertiesChangedAction a)
                    {
                        // Kinda a hack to prevent a jumping glitch
                        a.SetPostExecutionAction(null);
                        ContextActionManager.UndoAction();
                        a.SetPostExecutionAction((undo) =>
                        {
                            selection.UpdateRenderModel();
                        });
                        ContextActionManager.ExecuteAction(a);
                    }
                }

                LastUncommittedAction = null;
                ChangingPropery = null;
                ChangingObject = null;
            }
        }

        private void PropEditorParamRow(MapObject selection)
        {
            List<PARAM.Cell> cells = new List<PARAM.Cell>();
            if (selection.MsbObject is PARAM.Row row)
            {
                cells = row.Cells;

            }
            else if (selection.MsbObject is MergedParamRow mrow)
            {
                cells = mrow.Cells;
            }
            ImGui.Columns(2);
            ImGui.Separator();
            int id = 0;

            // This should be rewritten somehow it's super ugly
            var nameProp = selection.MsbObject.GetType().GetProperty("Name");
            var idProp = selection.MsbObject.GetType().GetProperty("ID");
            object oval = nameProp.GetValue(selection.MsbObject);
            object nval = null;
            ImGui.PushID(id);
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Name");
            ImGui.NextColumn();
            ImGui.SetNextItemWidth(-1);
            bool ch = PropertyRow(nameProp.PropertyType, oval, out nval);
            bool cm = ImGui.IsItemDeactivatedAfterEdit();
            ChangeProperty(nameProp, selection, selection.MsbObject, nval, ch, cm, false);
            ImGui.NextColumn();
            ImGui.PopID();
            id++;

            oval = idProp.GetValue(selection.MsbObject);
            nval = null;
            ImGui.PushID(id);
            ImGui.AlignTextToFramePadding();
            ImGui.Text("ID");
            ImGui.NextColumn();
            ImGui.SetNextItemWidth(-1);
            ch = PropertyRow(idProp.PropertyType, oval, out nval);
            cm = ImGui.IsItemDeactivatedAfterEdit();
            ChangeProperty(idProp, selection, selection.MsbObject, nval, ch, cm, false);
            ImGui.NextColumn();
            ImGui.PopID();
            id++;

            foreach (var cell in cells)
            {
                ImGui.PushID(id);
                ImGui.AlignTextToFramePadding();
                ImGui.Text(cell.Name);
                ImGui.NextColumn();
                ImGui.SetNextItemWidth(-1);
                //ImGui.AlignTextToFramePadding();
                var typ = cell.Value.GetType();
                var oldval = cell.Value;
                bool shouldUpdateVisual = false;
                bool changed = false;
                object newval = null;

                changed = PropertyRow(typ, oldval, out newval);
                bool committed = ImGui.IsItemDeactivatedAfterEdit();
                ChangeProperty(cell.GetType().GetProperty("Value"), selection, cell, newval, changed, committed, shouldUpdateVisual);

                ImGui.NextColumn();
                ImGui.PopID();
                id++;
            }
            ImGui.Columns(1);
        }

        private void PropEditorGeneric(MapObject selection, object target=null, bool decorate=true)
        {
            var obj = (target == null) ? selection.MsbObject : target;
            var type = obj.GetType();
            if (!PropCache.ContainsKey(type.FullName))
            {
                PropCache.Add(type.FullName, type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            }
            var properties = PropCache[type.FullName];
            if (decorate)
            {
                ImGui.Columns(2);
                ImGui.Separator();
            }
            int id = 0;
            foreach (var prop in properties)
            {
                if (!prop.CanWrite && !prop.PropertyType.IsArray)
                {
                    continue;
                }

                ImGui.PushID(id);
                ImGui.AlignTextToFramePadding();
                //ImGui.AlignTextToFramePadding();
                var typ = prop.PropertyType;
                if (typ.IsClass && typ != typeof(string) && !typ.IsArray)
                {
                    bool open = ImGui.TreeNodeEx(prop.Name, ImGuiTreeNodeFlags.DefaultOpen);
                    ImGui.NextColumn();
                    ImGui.SetNextItemWidth(-1);
                    var o = prop.GetValue(obj);
                    ImGui.Text(o.GetType().Name);
                    ImGui.NextColumn();
                    if (open)
                    {
                        PropEditorGeneric(selection, o, false);
                        ImGui.TreePop();
                    }
                    ImGui.PopID();
                }
                else if (typ.IsArray)
                {
                    Array a = (Array)prop.GetValue(obj);
                    for (int i = 0; i < a.Length; i++)
                    {
                        ImGui.PushID(i);

                        ImGui.Text($@"{prop.Name}[{i}]");
                        ImGui.NextColumn();
                        ImGui.SetNextItemWidth(-1);
                        var oldval = a.GetValue(i);
                        bool shouldUpdateVisual = false;
                        bool changed = false;
                        object newval = null;

                        changed = PropertyRow(typ.GetElementType(), oldval, out newval);
                        bool committed = ImGui.IsItemDeactivatedAfterEdit();
                        ChangeProperty(prop, selection, obj, newval, changed, committed, shouldUpdateVisual, i);

                        ImGui.NextColumn();
                        ImGui.PopID();
                    }
                    ImGui.PopID();
                }
                else
                {
                    ImGui.Text(prop.Name);
                    ImGui.NextColumn();
                    ImGui.SetNextItemWidth(-1);
                    var oldval = prop.GetValue(obj);
                    bool shouldUpdateVisual = false;
                    bool changed = false;
                    object newval = null;

                    changed = PropertyRow(typ, oldval, out newval);
                    bool committed = ImGui.IsItemDeactivatedAfterEdit();
                    ChangeProperty(prop, selection, obj, newval, changed, committed, shouldUpdateVisual);

                    ImGui.NextColumn();
                    ImGui.PopID();
                }
                id++;
            }
            if (decorate)
            {
                ImGui.Columns(1);
            }
        }

        public void OnGui(MapObject selection, float w, float h)
        {
            ImGui.SetNextWindowSize(new Vector2(350, h - 80), ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowPos(new Vector2(w - 370, 20), ImGuiCond.FirstUseEver);
            ImGui.Begin("Properties");
            if (selection == null)
            {
                ImGui.Text("Select a single object to edit properties.");
                ImGui.End();
                return;
            }
            if (selection.MsbObject is PARAM.Row prow || selection.MsbObject is MergedParamRow)
            {
                PropEditorParamRow(selection);
            }
            else
            {
                PropEditorGeneric(selection);
            }
            ImGui.End();
        }
    }
}