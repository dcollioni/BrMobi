namespace BrMobi.Core.Resources
{
    public class ResourceHelper
    {
        /// <summary>
        /// Você deve informar um e-mail válido.
        /// </summary>
        /// <returns>Você deve informar um e-mail válido.</returns>
        public static string InvalidEmail()
        {
            return Messages.InvalidEmail;
        }

        /// <summary>
        /// Seu e-mail já foi registrado.
        /// </summary>
        /// <returns>Seu e-mail já foi registrado.</returns>
        public static string AlreadyRegisteredEmail()
        {
            return Messages.AlreadyRegisteredEmail;
        }
    }
}