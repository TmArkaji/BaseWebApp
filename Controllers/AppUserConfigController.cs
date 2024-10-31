using AutoMapper;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using BaseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApplication.Controllers
{
    public class AppUserConfigController : BaseController<IAppUserConfigRepository, AppUserConfigVM, AppUserConfig, int>
    {
        private readonly IAppUserConfigRepository _repository;
        private readonly IMapper _mapper;
        public AppUserConfigController(IAppUserConfigRepository repository, IMapper mapper, ICryptoParamsProtector protector) : base(repository, mapper, protector)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<IActionResult> Edit(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            var model = _mapper.Map<AppUserConfigVM>(entity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, AppUserConfigVM model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _mapper.Map<AppUserConfig>(model);
                    await _repository.UpdateAsync(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.Exist(id))
                    {
                        return NotFound(ModelState);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
