using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public enum SelfTransitionMode
    {
        SELF_TRANSITION_MODE_CONTINUE_IF_CYCLIC_BLEND_IF_ACYCLIC = 0,
        SELF_TRANSITION_MODE_CONTINUE = 1,
        SELF_TRANSITION_MODE_RESET = 2,
        SELF_TRANSITION_MODE_BLEND = 3,
    }
    
    public enum EventMode
    {
        EVENT_MODE_DEFAULT = 0,
        EVENT_MODE_PROCESS_ALL = 1,
        EVENT_MODE_IGNORE_FROM_GENERATOR = 2,
        EVENT_MODE_IGNORE_TO_GENERATOR = 3,
    }
    
    public class hkbTransitionEffect : hkbGenerator
    {
        public SelfTransitionMode m_selfTransitionMode;
        public EventMode m_eventMode;
    }
}
