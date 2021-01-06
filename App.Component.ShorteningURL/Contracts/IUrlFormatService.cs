using System;
using System.Collections.Generic;
using System.Text;

namespace App.Component.ShorteningURL.Contracts
{
    public interface IUrlFormatService
    {
        string FormateURL(string url);
    }
}
