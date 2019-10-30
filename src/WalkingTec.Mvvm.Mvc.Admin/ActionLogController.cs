using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Auth.Attribute;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Mvc.Admin.ViewModels.ActionLogVMs;

namespace WalkingTec.Mvvm.Admin.Api
{
    [AuthorizeJwtWithCookie]
    [ActionDescription("Log")]
    [ApiController]
    [Route("api/_[controller]")]
    public class ActionLogController : BaseApiController
    {
        [ActionDescription("搜索")]
        [HttpPost("[action]")]
        public string Search(ActionLogSearcher searcher)
        {
            var vm = CreateVM<ActionLogListVM>();
            vm.Searcher = searcher;
            return vm.GetJson();
        }

        [ActionDescription("获取")]
        [HttpGet("{id}")]
        public ActionLogVM Get(Guid id)
        {
            var vm = CreateVM<ActionLogVM>(id);
            return vm;
        }

        [HttpPost("[action]")]
        [ActionDescription("删除")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = CreateVM<ActionLogBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("导出")]
        [HttpPost("[action]")]
        public IActionResult ExportExcel(ActionLogSearcher searcher)
        {
            var vm = CreateVM<ActionLogListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            var data = vm.GenerateExcel();
            return File(data, "application/vnd.ms-excel", $"Export_ActionLog_{DateTime.Now.ToString("yyyy-MM-dd")}.xls");
        }

        [ActionDescription("勾选导出")]
        [HttpPost("[action]")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = CreateVM<ActionLogListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            var data = vm.GenerateExcel();
            return File(data, "application/vnd.ms-excel", $"Export_ActionLog_{DateTime.Now.ToString("yyyy-MM-dd")}.xls");
        }
    }
}
