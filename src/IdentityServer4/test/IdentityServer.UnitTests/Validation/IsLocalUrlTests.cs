using FluentAssertions;
using IdentityServer.UnitTests.Common;
using IdentityServer4.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace IdentityServer.UnitTests.Validation
{
    public class IsLocalUrlTests
    {
        private const string queryParameters = "?client_id=mvc.code" +
        "&redirect_uri=https%3A%2F%2Flocalhost%3A44302%2Fsignin-oidc" +
        "&response_type=code" +
        "&scope=openid%20profile%20email%20custom.profile%20resource1.scope1%20resource2.scope1%20offline_access" +
        "&code_challenge=LcJN1shWmezC0J5EU7QOi7N_amBuvMDb6PcTY0sB2YY" +
        "&code_challenge_method=S256" +
        "&response_mode=form_post" +
        "&nonce=nonce" +
        "&state=state";
        public static IEnumerable<object[]> TestCases =>
            new List<object[]>
            {
            new object[] { "/connect/authorize/callback" + queryParameters, true },
            new object[] { "//evil.com/" + queryParameters, false },
            // Tab character
            new object[] { "/\t/evil.com/connect/authorize/callback" + queryParameters, false },
            // Tabs and Spaces
            new object[] { "/ \t/evil.com/connect/authorize/callback" + queryParameters, false },
            new object[] { "/  \t/evil.com/connect/authorize/callback" + queryParameters, false },
            new object[] { "/   \t/evil.com/connect/authorize/callback" + queryParameters, false },
            new object[] { "/\t /evil.com/connect/authorize/callback" + queryParameters, false },
            new object[] { "/\t  /evil.com/connect/authorize/callback" + queryParameters, false },
            new object[] { "/\t   /evil.com/connect/authorize/callback" + queryParameters, false },
            // Various new line related things
            new object[] { "/\n/evil.com/" + queryParameters, false },
            new object[] { "/\n\n/evil.com/" + queryParameters, false },
            new object[] { "/\r/evil.com/" + queryParameters, false },
            new object[] { "/\r\r/evil.com/" + queryParameters, false },
            new object[] { "/\r\n/evil.com/" + queryParameters, false },
            new object[] { "/\r\n\r\n/evil.com/" + queryParameters, false },
            // Tabs and Newlines
            new object[] { "/\t\n/evil.com/" + queryParameters, false },
            new object[] { "/\t\n\n/evil.com/" + queryParameters, false },
            new object[] { "/\t\r/evil.com/" + queryParameters, false },
            new object[] { "/\t\r\r/evil.com/" + queryParameters, false },
            new object[] { "/\t\r\n/evil.com/" + queryParameters, false },
            new object[] { "/\t\r\n\r\n/evil.com/" + queryParameters, false },
            new object[] { "/\n/evil.com\t/" + queryParameters, false },
            new object[] { "/\n\n/evil.com\t/" + queryParameters, false },
            new object[] { "/\r/evil.com\t/" + queryParameters, false },
            new object[] { "/\r\r/evil.com\t/" + queryParameters, false },
            new object[] { "/\r\n/evil.com\t/" + queryParameters, false },
            new object[] { "/\r\n\r\n/evil.com\t/" + queryParameters, false },
            };
        [Theory]
        [MemberData(nameof(TestCases))]
        public async void GetAuthorizationContextAsync(string returnUrl, bool expected)
        {
            var interactionService = new DefaultIdentityServerInteractionService(null, null, null, null, null, null, null,
                GetReturnUrlParser(), new LoggerFactory().CreateLogger<DefaultIdentityServerInteractionService>());
            var actual = await interactionService.GetAuthorizationContextAsync(returnUrl);
            if (expected)
            {
                actual.Should().NotBeNull();
            }
            else
            {
                actual.Should().BeNull();
            }
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public void IsLocalUrl(string returnUrl, bool expected)
        {
            returnUrl.IsLocalUrl().Should().Be(expected);
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public void GetIdentityServerRelativeUrl(string returnUrl, bool expected)
        {
            var serverUrls = new MockServerUrls
            {
                Origin = "https://localhost:5001",
                BasePath = "/"
            };
            var actual = serverUrls.GetIdentityServerRelativeUrl(returnUrl);
            if (expected)
            {
                actual.Should().NotBeNull();
            }
            else
            {
                actual.Should().BeNull();
            }
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public async void OidcReturnUrlParser_ParseAsync(string returnUrl, bool expected)
        {
            var oidcParser = GetOidcReturnUrlParser();
            var actual = await oidcParser.ParseAsync(returnUrl);
            if (expected)
            {
                actual.Should().NotBeNull();
            }
            else
            {
                actual.Should().BeNull();
            }
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public void OidcReturnUrlParser_IsValidReturnUrl(string returnUrl, bool expected)
        {
            var oidcParser = GetOidcReturnUrlParser();
            oidcParser.IsValidReturnUrl(returnUrl).Should().Be(expected);
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public void ReturnUrlParser_IsValidReturnUrl(string returnUrl, bool expected)
        {
            var parser = GetReturnUrlParser();
            parser.IsValidReturnUrl(returnUrl).Should().Be(expected);
        }
        [Theory]
        [MemberData(nameof(TestCases))]
        public async void ReturnUrlParser_ParseAsync(string returnUrl, bool expected)
        {
            var parser = GetReturnUrlParser();
            var actual = await parser.ParseAsync(returnUrl);
            if (expected)
            {
                actual.Should().NotBeNull();
            }
            else
            {
                actual.Should().BeNull();
            }
        }
        private static ReturnUrlParser GetReturnUrlParser()
        {
            var oidcParser = GetOidcReturnUrlParser();
            var parser = new ReturnUrlParser(new IReturnUrlParser[] { oidcParser });
            return parser;
        }
        private static OidcReturnUrlParser GetOidcReturnUrlParser()
        {
            return new OidcReturnUrlParser(
                new StubAuthorizeRequestValidator
                {
                    Result = new AuthorizeRequestValidationResult
                    (
                        new ValidatedAuthorizeRequest()
                    )
                },
                new MockUserSession(),
                new LoggerFactory().CreateLogger<OidcReturnUrlParser>(),
                new MockAuthorizationParametersMessageStore()
                );
            //new StubAuthorizeRequestValidator
            //{
            //    Result = new AuthorizeRequestValidationResult
            //    (
            //        new ValidatedAuthorizeRequest()
            //    )
            //},
            //new MockUserSession(),
            //new MockServerUrls(),
            //new LoggerFactory().CreateLogger<OidcReturnUrlParser>());
        }
    }

    public class StubAuthorizeRequestValidator : IAuthorizeRequestValidator
    {
        public AuthorizeRequestValidationResult Result { get; set; }
        public Task ValidateAsync(ValidatedAuthorizeRequest request)
        {
            return Task.CompletedTask;
        }

        public Task<AuthorizeRequestValidationResult> ValidateAsync(NameValueCollection parameters, ClaimsPrincipal subject = null)
        {
            return Task.FromResult(Result);
        }
    }

    public class MockAuthorizationParametersMessageStore : IAuthorizationParametersMessageStore
    {
        public Task DeleteAsync(string id)
        {
            return Task.CompletedTask;
        }

        public Task<Message<IDictionary<string, string[]>>> ReadAsync(string id)
        {
            return Task.FromResult(default(Message<IDictionary<string, string[]>>));
        }

        public Task<string> WriteAsync(Message<IDictionary<string, string[]>> message)
        {
            return Task.FromResult("");
        }
    }

    public class MockServerUrls
    {
        public string Origin { get; set; }
        public string BasePath { get; set; }
        public string GetIdentityServerRelativeUrl(string returnUrl)
        {
            if (returnUrl.IsLocalUrl())
            {
                return returnUrl;
            }
            return null;
        }
    }
}
