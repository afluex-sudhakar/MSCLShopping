using System.Collections.Generic;

namespace Razorpay.Api
{
    public class CustomerRaz : Entity 
    {
        public CustomerRaz(string customerId = "")
        {
            this["id"] = customerId;
        }

        new public CustomerRaz Fetch(string id)
        {
            return (CustomerRaz)base.Fetch(id);
        }

        public CustomerRaz Create(Dictionary<string, object> data)
        {
            string relativeUrl = GetEntityUrl();
            List<Entity> entities = Request(relativeUrl, HttpMethod.Post, data);
            return (CustomerRaz)entities[0];
        }
           
        public CustomerRaz Edit(Dictionary<string, object> data) 
        {
            string relativeUrl = string.Format("{0}/{1}", GetEntityUrl(), this["id"]);
            List<Entity> entities = Request(relativeUrl, HttpMethod.Put, data);
            return (CustomerRaz)entities[0];
        }

        public Token Token(string tokenId) 
        {
            string relativeUrl = string.Format("{0}/{1}/tokens/{2}", GetEntityUrl(), this["id"], tokenId);
            List<Entity> entities = Request(relativeUrl, HttpMethod.Get, null);
            return (Token)entities[0];
        }

        /**
         * Fetch multiple tokens associated with the customerId
        **/
        public List<Token> Tokens() 
        {
            string relativeUrl = string.Format("{0}/{1}/tokens", GetEntityUrl(), this["id"]);
            List<Entity> entities = Request(relativeUrl, HttpMethod.Get, null);

            List<Token> tokens = new List<Token>();

            foreach(Entity entity in entities)
            {
                tokens.Add(entity as Token);
            }

            return tokens;
        }

        public void DeleteToken(string tokenId)
        {
            string relativeUrl = string.Format("{0}/{1}/tokens/{2}", GetEntityUrl(), this["id"], tokenId);
            Request(relativeUrl, HttpMethod.Delete, null);
        }
    }
}