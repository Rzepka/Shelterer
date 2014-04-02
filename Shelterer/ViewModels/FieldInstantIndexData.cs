using Shelterer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shelterer.ViewModels
{
    public class FieldInstantIndexData
    {
        public IEnumerable<FieldInstant> FieldInstants { get; set; }

        public Dictionary<string, int> DataTypeIds {
            get { return DbRecord.DataTypeIds; }
        }

        public Dictionary<int, Tuple<string, Dictionary<int, string>>> TableFieldIds 
        {
            get//= (() => 
              {
                var dict = new Dictionary<int, Tuple<string, Dictionary<int, string>>>();
                foreach(var kv in DbRecord.TableFieldIds)
                {
                    var d = new Dictionary<int, string>();
                    foreach(var i in kv.Value.Item2)
                    {
                        d.Add(i.Value, i.Key);
                    }
                    dict.Add(kv.Value.Item1, Tuple.Create(kv.Key, d));
                }
                return dict;
            }//);
        }
    }
}