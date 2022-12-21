namespace MemesApi.Services;

public class MlModelService : IModelService
{
    public string Url { get; private set; }

    public MlModelService(string url)
    {
        Url = url;
    }
    
    public Task<Stream> SendToModelAsync(Stream stream)
    {
        throw new NotImplementedException();
    }
}