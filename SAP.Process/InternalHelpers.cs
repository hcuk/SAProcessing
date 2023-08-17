/*
    Copyright 2016 Healthcare Communications UK Ltd
 
    This file is part of HCSentimentAnalysisProcessor.

    HCSentimentAnalysisProcessor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HCSentimentAnalysisProcessor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HCSentimentAnalysisProcessor.  If not, see <http://www.gnu.org/licenses/>.

 */

using System.Text.RegularExpressions;

namespace SAP.Process
{
    /// <summary>
    /// internally accessible helpers for Engine
    /// </summary>
  public static class InternalHelpers
  {
    internal static string FormatSentenceForSplit(this string sentence)
    {
      string input = Regex.Replace(sentence, "(?<![:;'])(\\()(?![:;'])|(?<![:;'])(\\))(?![:;'])", "", RegexOptions.IgnoreCase);
      MatchCollection matchCollection = Regex.Matches(input, "[\\p{P}-[:;'()]]");
      int num = 0;
      foreach (Match match in matchCollection)
      {
        input = input.Insert(match.Index + num, " ");
        ++num;
      }
      return Regex.Replace(input.Trim(), "\\s+", " ", RegexOptions.Multiline);
    }
  }
}
