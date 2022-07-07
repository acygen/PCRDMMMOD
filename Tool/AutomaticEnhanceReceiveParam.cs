using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRCalculator.Tool
{
    public class AutomaticEnhanceReceiveParam
    {
        public DataHead data_headers = new DataHead(1);
        public AutoDataBody data = new AutoDataBody();

        
    }
    public class AutoDataBody
    {
        public UnitDataS unit_data = new UnitDataS();
    }
	public class AutomaticEnhancePostParam
	{
        public int unit_id;		
	}
    
}
