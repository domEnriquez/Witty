using System.ComponentModel.DataAnnotations;
using Witty.Constants;

namespace Witty.ViewModels
{
    public class ValidResponseCategory : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string category = value.ToString();

            if (category == Messenger.Sarcasm
                || category == Messenger.SelfDeprecation
                || category == Messenger.Exaggeration
                || category == Messenger.Shock
                || category == Messenger.WordPlay
                || category == Messenger.Association
                || category == Messenger.Reverse
                || category == Messenger.Misdirect
                || category == Messenger.Analogy) 
                return true;
            else
                return false;
        }

    }
}
