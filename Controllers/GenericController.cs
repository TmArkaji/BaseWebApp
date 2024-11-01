using BaseWebApplication.Data;
using BaseWebApplication.Models;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BaseWebApplication.Controllers
{
    public abstract class BaseController<TRepository, TViewModel, TModel, TKey> : Controller
        where TRepository : IGenericRepository<TKey, TModel>
        where TModel : BaseEntity<TKey>
        where TViewModel : BaseIdentityVM<TKey>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICryptoParamsProtector _protector;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public BaseController(TRepository repository, IMapper mapper, ICryptoParamsProtector protector, IStringLocalizer<SharedResource> localizer)
        {
            _repository = repository;
            _mapper = mapper;
            _protector = protector;
            _localizer = localizer;
        }

        public virtual async Task<IActionResult> Index()
        {
            var model = await _repository.GetAllAsync();
            var vModel = _mapper.Map<List<TViewModel>>(model);
            foreach (var item in vModel) item.encryptedID = _protector.EncryptParamDictionary(
                new Dictionary<string, string> {
                    {"ID", item.ID.ToString() }
                }
            );
            return View(vModel);
        }

        [CryptoValueProvider]
        public virtual async Task<IActionResult> Details(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            var vModel = _mapper.Map<TViewModel>(entity);
            vModel.encryptedID = _protector.EncryptParamDictionary(
                new Dictionary<string, string> {
                    {"ID", vModel.ID.ToString() }
                });
            return View(vModel);
        }

        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<TModel>(model);
                await _repository.CreateAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [CryptoValueProvider]
        public virtual async Task<IActionResult> Edit(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return View(_mapper.Map<TViewModel>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(TKey id, TViewModel model)
        {
            //if (id != model.ID)
            //{
            //    return NotFound();
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _mapper.Map<TModel>(model);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(IFormCollection form)
        {
            string encryptedID = form["id"];
            if (!string.IsNullOrEmpty(encryptedID))
            {
                var dictionary = _protector.DecryptToParamDictionary(encryptedID);
                string sID = dictionary["id"];
                TKey id = (TKey)Convert.ChangeType(sID, typeof(TKey));
                await _repository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
