namespace Admin.Interfaces.ModelBuilders
{
    public interface IModelBuilder<TModel, in TSource>
    {
        TModel Build();
        TModel Build(TSource source);
        TModel Rebuild(TModel model);
    }
}
