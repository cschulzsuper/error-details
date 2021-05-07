using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Super.Core.ErrorDetails.Filters;
using Super.Core.ErrorDetails.Options;

namespace Super.Core.ErrorDetails
{
    public class ErrorDetailFilterProvider : IErrorDetailFilterProvider
    {
        private readonly IOptions<ErrorDetailsOptions> _errorDetailsOptions;
        private readonly IServiceProvider _serviceProvider;

        public ErrorDetailFilterProvider(
            IOptions<ErrorDetailsOptions> errorDetailsOptions,
            IServiceProvider serviceProvider)
        {
            _errorDetailsOptions = errorDetailsOptions;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IErrorDetailFilter> GetAll()
        {
            var errorDetailFilterDescriptors = _errorDetailsOptions.Value.Filters;

            foreach (var errorDetailFilterDescriptor in errorDetailFilterDescriptors)
            {
                var errorDetailFilterType = errorDetailFilterDescriptor.Type;
                var errorDetailFilterArguments = errorDetailFilterDescriptor.Arguments;
                var errorDetailFilter = null as IErrorDetailFilter;

                if (!errorDetailFilterDescriptor.Arguments.Any())
                {
                    errorDetailFilter = GetServiceOrDefault(errorDetailFilterType);
                }

                errorDetailFilter ??= CreateInstanceOrDefault(errorDetailFilterType, errorDetailFilterArguments);

                if (errorDetailFilter != null)
                {
                    yield return errorDetailFilter;
                }
            }
        }

        public IErrorDetailFilter? CreateInstanceOrDefault(Type type, object[] arguments)
        {
            if (type.IsInterface)
            {
                return null;
            }

            return ActivatorUtilities.CreateInstance(_serviceProvider, type, arguments) as IErrorDetailFilter;
        }

        public IErrorDetailFilter? GetServiceOrDefault(Type errorDetailFilterType)
        {
            return _serviceProvider.GetService(errorDetailFilterType) as IErrorDetailFilter;
        }
    }
}
