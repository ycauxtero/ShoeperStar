using HashidsNet;

namespace ShoeperStar.Data.Services
{
    public class HashIdService
    {
        private readonly string _salt;
        private readonly Hashids _hashId;

        public HashIdService(string salt)
        {
            _salt = salt;
            _hashId = new Hashids(_salt);
        }

        public string EncodeInt(int id)
        {
            return _hashId.Encode(id);
        }

        public int Decode(string hash)
        {

            try
            {
                var id = _hashId.Decode(hash)[0];

                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }

    public class HashId
    {
        public string Salt { get; set; }
    }
}
