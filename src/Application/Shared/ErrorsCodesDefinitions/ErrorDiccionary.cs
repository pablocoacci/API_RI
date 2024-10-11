namespace Application.Shared.ErrorsCodesDefinitions
{
    public static class ErrorDiccionary
    {
        private static Dictionary<ErrorCodes, string> _errorDescription = new Dictionary<ErrorCodes, string>()
        {
            { ErrorCodes.OK, "Se ejecuto correctamente" },
            { ErrorCodes.NotDefined, "Error no definido" },
            { ErrorCodes.UserNotCreated, "Error al crear el nuevo usuario" },
            { ErrorCodes.InvalidUserNameOrPassword, "El user o el password no son validos" },
            { ErrorCodes.UserAccountNotConfirmed, "Cuenta de usuario no confirmada" },
            { ErrorCodes.UserNotExist, "El usuario no existe" },
            { ErrorCodes.CanNotConfirmAccount, "Error al confirmar la cuenta" },
            { ErrorCodes.NewPasswordIsSameOld, "La nueva password es igual a la anterior" },
            { ErrorCodes.CanNotResetPassword, "Error al setear la nueva password" }
        };

        public static string GetErrorDescription(ErrorCodes code)
            => _errorDescription[code];
    }
}
