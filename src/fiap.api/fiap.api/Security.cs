using System.Text;

namespace fiap.api
{
    public static class Security
    {
        public static byte[] GetKey() => Encoding.UTF8.GetBytes("frase maior do que 16 caracteres");
    }
}
