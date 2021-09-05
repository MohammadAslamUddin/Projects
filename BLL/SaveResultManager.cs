using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Gateway;
using Projects.Models;

namespace Projects.BLL
{
    public class SaveResultManager
    {
        private SaveResultGateway saveResultGateway;

        public SaveResultManager()
        {
            saveResultGateway = new SaveResultGateway();
        }

        public List<SelectListItem> GetAllGrades()
        {
            return saveResultGateway.GetAllGrades();
        }

        public string Save(SaveResult result)
        {
            if (saveResultGateway.IsResultExist(result))
            {
                int rowAffected = saveResultGateway.UpdateResult(result);
                if (rowAffected>0)
                {
                    return "Result Updated!";
                }
                else
                {
                    return "Updating Result Failed!";
                }
            }
            else
            {
                int rowAffected = saveResultGateway.Save(result);
                if (rowAffected> 0)
                {
                    return "Saved!";
                }
                else
                {
                    return "Saving Failed!";
                }
            }
        }
    }
}