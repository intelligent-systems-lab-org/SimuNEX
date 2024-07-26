namespace SimuNEX
{
    public interface IModel
    {
        IModelOutput[] outports { get; }
        IModelInput[] inports { get; }
    }
}
