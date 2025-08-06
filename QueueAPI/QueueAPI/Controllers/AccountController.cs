using QueueAPI.Models.DTO;
using QueueAPI.Models;
using QueueAPI.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Helpers;
using QueueAPI.Helpers;


namespace QueueAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        AccountUtil accountUtil = new AccountUtil();

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginRequestModel dto)
        {
            try
            {
                accountUtil.username = dto.Username;
                AccountModel accountModel = accountUtil.AccountGet();

                if (accountModel != null)
                {
                    CryptoHelper cryptoHelper = new CryptoHelper();
                    string hashedPassword = cryptoHelper.HashSHA256(dto.Password, accountModel.password_salt);

                    accountUtil.password = hashedPassword;

                    AccountModel user = accountUtil.Login();

                    if (user != null)
                    {
                        var roles = new string[] { Roles.Admin };
                        var jwtSecurityToken = JWTAuthentication.GenerateJwtToken(dto.Username, roles.ToList());
                        var validUserName = JWTAuthentication.ValidateToken(jwtSecurityToken);
                        return Ok(new { token = jwtSecurityToken, account_id = user.account_id });
                    }

                    return BadRequest("Incorrect password");
                }

                return BadRequest("Username not found");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public IHttpActionResult SignUp([FromBody] AccountCreateRequestModel dto)
        {
            return Ok();
        }
    }
}