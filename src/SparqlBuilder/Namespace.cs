using System.Dynamic;

namespace MAP.SPARQL.Builder;

public class Namespace : DynamicObject
{
    private Uri _baseUri;

    public Namespace(Uri baseUri) => _baseUri = baseUri;
    public Namespace(string baseUri) => _baseUri = new Uri(baseUri);


    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = $"<{new Uri(_baseUri, binder.Name).ToString()}>";
        return true;
    }
}