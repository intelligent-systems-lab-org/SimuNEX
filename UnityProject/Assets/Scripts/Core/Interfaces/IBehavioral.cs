using System.Collections.Generic;

namespace SimuNEX
{
    public interface IBehavioral
    {
    }

    public abstract class Dynamics : IBehavioral
    {
    }

    public class StateSpace : IBehavioral
    {
    }

    public class PortMap : IBehavioral
    {
        protected List<Model> components = new();
    }
}
