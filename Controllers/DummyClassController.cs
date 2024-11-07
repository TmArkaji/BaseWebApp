using AutoMapper;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using BaseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BaseWebApplication.Controllers
{
    public class DummyClassController : BaseController<IDummyClassRepository, DummyClassVM, DummyClass, string>
    {
        private readonly IDummyClassRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;
        private readonly IDummyClassTypeRepository _dummyClassTypeRepository;

        public DummyClassController(IDummyClassRepository repository, IMapper mapper, ICryptoParamsProtector protector, IStringLocalizer<SharedResource> localizer, IDummyClassTypeRepository dummyClassTypeRepository) : base(repository, mapper, protector, localizer)
        {
            _repository = repository;
            _mapper = mapper;
            _localizer = localizer;
            _dummyClassTypeRepository = dummyClassTypeRepository;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(DummyClassVM model)
        {
            //model.ID = "-1";
            ModelState.Remove(nameof(model.ID));
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<DummyClass>(model);
                await _repository.CreateAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            LoadViewBag(false);
            return View(model);
        }

        public override void LoadViewBag(bool edit = false)
        {
            ViewBag.DummyClassTypeID = _dummyClassTypeRepository.GetDropDownList(
                x => x.ClassType,
                x => x.ID,
                x => !x.Eliminado
            ).GetAwaiter().GetResult();
        }
    }
}
