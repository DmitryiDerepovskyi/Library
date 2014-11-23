using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core;
namespace Library.App.Catalog
{
    public interface ISerializedAsync<T> where T:PrintedMatter
    {
        MyListArray<T> Deserialized(string path);

        Task SerializedAsync(MyListArray<PrintedMatter> items, string path);
    }
}
