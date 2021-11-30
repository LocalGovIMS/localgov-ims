﻿using Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.ImportProcessingRuleCondition;

namespace Admin.Controllers
{
    public interface IImportProcessingRuleConditionControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        //IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, CreateViewModelBuilderArgs> CreateViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
        IModelCommand<int> DeleteCommand { get; }

        #endregion
    }
}
