using Sitecore.Globalization;
using Sitecore.Pipelines.PreprocessRequest;
using System.Globalization;

namespace Sitecore.Support.Pipelines.PreprocessRequest
{
  public class StripLanguage : Sitecore.Pipelines.PreprocessRequest.StripLanguage
  {
    protected override bool IsValidForStrippingFromUrl([NotNull] Language language, [NotNull]PreprocessRequestArgs args)
    {
      var cultureInfo = language.CultureInfo;

      var isLocal_custom_unspecified = cultureInfo.LCID == 0x1000;

      var customCulture = isLocal_custom_unspecified || cultureInfo.CultureTypes.HasFlag(CultureTypes.UserCustomCulture);

      return !customCulture || this.IsCustomCultureAllowed(language);
    }

    protected virtual bool IsCustomCultureAllowed([NotNull] Language language)
    {
      return LanguageDefinitions.GetLanguageDefinition(language) != null;
    }
  }
}