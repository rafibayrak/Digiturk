using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MovieApp.Business.Extensions
{
    public static class Jwt
    {
        public static void CustomJwtAuthentication(this IServiceCollection services, byte[] key) {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    //JWT kullanacağım ve ayarları da şunlar olsun dediğimiz yer ise burasıdır.
                    .AddJwtBearer(x =>
                    {
                    //Gelen isteklerin sadece HTTPS yani SSL sertifikası olanları kabul etmesi(varsayılan true)
                    x.RequireHttpsMetadata = false;
                    //Eğer token onaylanmış ise sunucu tarafında kayıt edilir.
                    x.SaveToken = true;
                    //Token içinde neleri kontrol edeceğimizin ayarları.
                    x.TokenValidationParameters = new TokenValidationParameters
                        {
                        //Token 3.kısım(imza) kontrolü
                        ValidateIssuerSigningKey = true,
                        //Neyle kontrol etmesi gerektigi
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                        ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
        }
    }
}
