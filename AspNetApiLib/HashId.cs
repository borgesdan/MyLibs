using HashidsNet;

namespace MyAspNetApiLib
{
    public class HashId
    {
        // O "Salt" garante que os seus hashes sejam únicos para a sua aplicação.
        private const string Salt = "{B22A975F-8577-44E0-84B3-DD6A26E341EC}";
        private const int MinHashLength = 8; // Tamanho mínimo da string gerada

        readonly Hashids _hashids;

        public HashId() 
        {
            _hashids = new(Salt, MinHashLength);
        }

        public HashId(string salt, int minLength)
        {
            _hashids = new(salt, minLength);
        }

        public string Encode(int id)
        {
            return _hashids.Encode(id);
        }

        public int? Decode(string? hash)
        {
            if (hash == null)
                return null;

            int[] decoded = _hashids.Decode(hash);

            if (decoded.Length > 0)
                return decoded[0];

            return null; // Retorna null se a string for inválida
        }
    }
}
