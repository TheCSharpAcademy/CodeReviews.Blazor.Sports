using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Models;

public static class SignInErrors
{
    public static Error EmailRequired => Error.Validation(
        "SignIn.EmailRequired",
        "The email is required.");

    public static Error EmailInvalid => Error.Validation(
        "SignIn.EmailInvalid",
        "A valid email address is required.");

    public static Error PasswordRequired => Error.Validation(
        "SignIn.PasswordRequired",
        "The password is required.");
}

public static class DemoSignInErrors
{
    public static Error EmailRequired => Error.Validation(
        "DemoSignIn.EmailRequired",
        "The email is required.");

    public static Error EmailInvalid => Error.Validation(
        "DemoSignIn.EmailInvalid",
        "A valid email address is required.");
}