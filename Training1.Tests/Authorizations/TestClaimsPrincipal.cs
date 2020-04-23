using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Training1.Areas.Identity.Data;

namespace Training1.Tests.Authorizations
{
    public class TestClaimsPrincipal : ClaimsPrincipal
    {
        public TestClaimsPrincipal()
            : base(new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, "homer.simpson")}))
        {

        }


        public TestClaimsPrincipal AddRole(string role)
        {
            ((ClaimsIdentity)this.Identity)
                .AddClaim(
                        new Claim(ClaimTypes.Role, role));
            return this;
        }
        public TestClaimsPrincipal AddStatus(Status status)
        {
            ((ClaimsIdentity)this.Identity)
                .AddClaim(
                        new Claim("AccountStatus", status.ToString()));
            return this;
        }
    }
}
