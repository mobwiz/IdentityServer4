// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace IdentityServer4.Extensions
{
    internal static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string ToSpaceSeparatedString(this IEnumerable<string> list)
        {
            if (list == null)
            {
                return string.Empty;
            }

            return string.Join(' ', list);
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> FromSpaceSeparatedString(this string input)
        {
            input = input.Trim();
            return input.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> ParseScopesString(this string scopes)
        {
            if (scopes.IsMissing())
            {
                return null;
            }

            scopes = scopes.Trim();
            List<string> list = scopes.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
            if (list.Any())
            {
                list.Sort();
                return list;
            }

            return null;
        }

        [DebuggerStepThrough]
        public static bool IsMissing([NotNullWhen(false)] this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static bool IsMissingOrTooLong(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            if (value.Length > maxLength)
            {
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsPresent([NotNullWhen(true)] this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static string EnsureLeadingSlash(this string url)
        {
            if (url != null && !url.StartsWith("/"))
            {
                return "/" + url;
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string EnsureTrailingSlash(this string url)
        {
            if (url != null && !url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string RemoveLeadingSlash(this string url)
        {
            if (url != null && url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string RemoveTrailingSlash(this string url)
        {
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string CleanUrlPath(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "/";
            }

            if (url != "/" && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static bool IsLocalUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            if (url[0] == '/')
            {
                if (url.Length == 1)
                {
                    return true;
                }

                if (url[1] != '/' && url[1] != '\\')
                {
                    return !HasControlCharacter(url.AsSpan(1));
                }

                return false;
            }

            if (url[0] == '~' && url.Length > 1 && url[1] == '/')
            {
                if (url.Length == 2)
                {
                    return true;
                }

                if (url[2] != '/' && url[2] != '\\')
                {
                    return !HasControlCharacter(url.AsSpan(2));
                }

                return false;
            }

            return false;
            static bool HasControlCharacter(ReadOnlySpan<char> readOnlySpan)
            {
                for (int i = 0; i < readOnlySpan.Length; i++)
                {
                    if (char.IsControl(readOnlySpan[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        [DebuggerStepThrough]
        public static bool IsUri(this string input)
        {
            if (!Uri.TryCreate(input, UriKind.Absolute, out Uri result))
            {
                return false;
            }

            if (result.IsFile && !input.StartsWith(Uri.UriSchemeFile + "://", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string name, string value)
        {
            return url.AddQueryString(name + "=" + UrlEncoder.Default.Encode(value));
        }

        [DebuggerStepThrough]
        public static string AddHashFragment(this string url, string query)
        {
            if (!url.Contains("#"))
            {
                url += "#";
            }

            return url + query;
        }

        [DebuggerStepThrough]
        public static NameValueCollection ReadQueryStringAsNameValueCollection(this string url)
        {
            if (url != null)
            {
                int num = url.IndexOf('?');
                if (num >= 0)
                {
                    url = url.Substring(num + 1);
                }

                Dictionary<string, StringValues> dictionary = QueryHelpers.ParseNullableQuery(url);
                if (dictionary != null)
                {
                    return dictionary.AsNameValueCollection();
                }
            }

            return new NameValueCollection();
        }

        public static string GetOrigin(this string url)
        {
            if (url != null)
            {
                Uri uri;
                try
                {
                    uri = new Uri(url);
                }
                catch (Exception)
                {
                    return null;
                }

                return uri.Scheme + "://" + uri.Authority;
            }

            return null;
        }

        public static string Obfuscate(this string value)
        {
            string text = "****";
            if (value.IsPresent() && value.Length > 4)
            {
                text = value.Substring(value.Length - 4);
            }

            return "****" + text;
        }
    }
}