using System.Collections.Generic;

namespace SimuNEX
{
    /// <summary>
    /// Defines a <see cref="Model"/> implementation.
    /// </summary>
    public interface IBehavioral
    {
        IModelOutput[] states { get; }
        IModelInput[] inputs { get; }
    }

    public class Super : IBehavioral
    {
        protected List<Model> components = new();

        public IModelPort[] ports => portMap();

        public IModelOutput[] states => throw new System.NotImplementedException();

        public IModelInput[] inputs => throw new System.NotImplementedException();

        public delegate IModelPort[] PortMap();

        public PortMap portMap;

        public Super(params IModelPort[] ports)
        {
            portMap = () => ports;
        }
    }

    public abstract class Dynamics : IBehavioral
    {
        public abstract IModelOutput[] states { get; }
        public abstract IModelInput[] inputs { get; }

        public abstract void Step(IModelOutput[] states, IModelInput[] inputs);
    }

    public class StateSpace : Dynamics
    {
        public override IModelOutput[] states { get; }

        public override IModelInput[] inputs { get; }

        public dynamic derivatives { get; }

        public override void Step(IModelOutput[] states, IModelInput[] inputs)
        {
            derivativeFcn(states, inputs);
        }

        public delegate void DerivativeFunction(IModelOutput[] states, IModelInput[] inputs);

        public DerivativeFunction derivativeFcn;
    }
}
