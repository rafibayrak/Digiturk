using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Dtos
{
    public class UserAuthJwtDto : BaseAuthDto
    {
        public string Token { get; set; }
    }
}
