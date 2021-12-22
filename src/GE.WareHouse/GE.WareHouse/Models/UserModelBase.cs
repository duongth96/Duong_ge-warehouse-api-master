using Dapper;
using GE.WareHouse.Common;
using GE.WareHouse.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GE.WareHouse.Models
{
    public enum UserStatus
    {
        NotVerified = 0,
        Verified = 1,
        Disabled = 2,
    }
    public class UserModel
    {
        public string Username { get; set; }
        public int Usermobi { get; set; }
        public UserStatus Status { get; set; }
        public object Id { get; set; }
    }
    public class UserModelADO
    {
        private SQLCon _sQLCon;
        private AppSettings _appSettings;
        public UserModelADO(AppSettings appSetting)
        {
            this._appSettings = appSetting;
            _sQLCon = new SQLCon(appSetting.SqlCnnString);
        }

        public UserModel GetById(int id)
        {
            string query = $"select * from _USERMOBI";

            DynamicParameters pr = new DynamicParameters();
            pr.Add("id", id);
            var rs = _sQLCon.ExecuteListDapperText<UserModel>(query, pr);
            return  rs.FirstOrDefault();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            int usermobi = -1;
            if (int.TryParse(model.Password, out int _usermobi))
            {
                usermobi = _usermobi;
            }

            string query = $"select * from _USERMOBI where [Status]<>{(int)UserStatus.Disabled} and Username='{model.Username}'";

            DynamicParameters pr = new DynamicParameters();
            var user = _sQLCon.ExecuteListDapperText<UserModel>(query, pr).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(UserModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
