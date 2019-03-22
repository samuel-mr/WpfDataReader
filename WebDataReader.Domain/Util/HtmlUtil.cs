using System;

namespace WebDataReader.Domain.Util
{
  public static class HtmlUtil
  {
    public static string TagWithoutAttributes(this string tag)
    {
      if (string.IsNullOrWhiteSpace(tag))
        return String.Empty;
      var firstSpace = tag.IndexOf(" ", StringComparison.InvariantCulture);
      var lastClose = tag.IndexOf(">", StringComparison.InvariantCulture);
      return tag.Substring(firstSpace + 1, tag.Length - (firstSpace + 1 + (tag.Length - lastClose)));
    }

    public static string InnerContentFromTag( this string tag)
    {
      var html = InnerHtmlFromTag(tag);
      if (html.Contains("<"))
        return InnerContentFromTag(html);

      return html;
    }
    public static string InnerHtmlFromTag(this string tag)
    {
      if (string.IsNullOrWhiteSpace(tag))
        return String.Empty;
      var isIniTag = false;
      var isIniTagFilled = false;
      var isIniTagWithAttributes = false;
      var principalNameTag = "";
      var content = "";

      for (int i = 0; i < tag.Length; i++)
      {
        var t = tag[i];

        if (isIniTagFilled)
        {
          if (tag.Substring(i, (principalNameTag.Length + 3)) == $"</{principalNameTag}>")
          {
            break;
          }
          content += t;
        }
        else if (isIniTag)
        {
          if (t == '>')
          {
            isIniTag = false;
            isIniTagFilled = true;
            continue;
          }

          if (t == ' ')
          {
            isIniTagWithAttributes = true;
          }
          if (!isIniTagWithAttributes)
            principalNameTag += t;
        }
        else if (t == '<')
        {
          isIniTag = true;
        }
      }

      return content.Trim();
    }
  }
}
