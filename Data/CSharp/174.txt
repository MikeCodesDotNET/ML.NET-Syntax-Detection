using Api.Database;
using GraphQL;

namespace Api.Graphql 
{
  [GraphQLMetadata("Mutation")]
  public class Mutation 
  {
    [GraphQLMetadata("addAuthor")]
    public Author Add(string name)
    {
      return null;
    }
  }
}