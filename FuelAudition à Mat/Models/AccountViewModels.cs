using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace FuelAudition.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class EmailNotConfirmViewModel
    {
        public string Email { get; set; }
        public bool Send { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "ConnexionCourriel", ResourceType = typeof(Resources.Resources))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConnexionMotPasse", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [Display(Name = "ConnexionRememberMe", ResourceType = typeof(Resources.Resources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "InscriptionCourriel", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

        
        [Display(Name = "InscriptionNom", ResourceType = typeof(Resources.Resources))]
        public string Nom { get; set; }

        [Display(Name = "InscriptionPrenom", ResourceType = typeof(Resources.Resources))]
        public string Prenom { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "InscriptionMotPasseErreurNbCaract", MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "InscriptionMotPasse", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "InscriptionConfirmationMotPasse", ResourceType = typeof(Resources.Resources))]
        [Compare("Password", ErrorMessageResourceName = "InscriptionConfirmationMotPasseErreur", ErrorMessageResourceType = typeof(Resources.Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
