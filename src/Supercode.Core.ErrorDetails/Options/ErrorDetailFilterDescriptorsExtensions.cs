using Supercode.Core.ErrorDetails.Filters;
using Supercode.Core.ErrorDetails.Providers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Supercode.Core.ErrorDetails.Options
{
    public static class ErrorDetailFilterDescriptorsExtensions
    {
        public static IList<ErrorDetailFilterDescriptor> TargetSite(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(TargetSiteFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> TargetSiteAsync(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(TargetSiteAsyncFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> TargetSiteContract(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(TargetSiteContractFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> TargetSiteDeclaringType(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(TargetSiteDeclaringTypeFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorCode(this IList<ErrorDetailFilterDescriptor> descriptors, string type)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorCodeFilter), type));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorCodeFromService<TService>(this IList<ErrorDetailFilterDescriptor> descriptors)
            where TService : class, IErrorCodeProvider<MemberInfo>
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorCodeFromServiceFilter<TService>)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorCodeFromAnnotation<TAttribute>(this IList<ErrorDetailFilterDescriptor> descriptors, Func<TAttribute, string> selector)
            where TAttribute : Attribute
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorCodeFromAnnotationFilter<TAttribute>), selector));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> InnerException(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(InnerExceptionFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> InnerExceptions(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(InnerExceptionsFilter)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorMessage(this IList<ErrorDetailFilterDescriptor> descriptors, string message)
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorMessageFilter), message));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorMessageFromException(this IList<ErrorDetailFilterDescriptor> descriptors)
        {
            descriptors.ErrorMessageFromException<Exception>(x => FormattableStringFactory.Create(x.Message));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorMessageFromException<TException>(this IList<ErrorDetailFilterDescriptor> descriptors, Func<TException, FormattableString> selector)
            where TException : Exception
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorMessageFromExceptionFilter<TException>), selector));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorMessageFromService<TService>(this IList<ErrorDetailFilterDescriptor> descriptors)
            where TService : class, IErrorMessageProvider<MemberInfo>
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorMessageFromServiceFilter<TService>)));
            return descriptors;
        }

        public static IList<ErrorDetailFilterDescriptor> ErrorMessageFromAnnotation<TAttribute>(this IList<ErrorDetailFilterDescriptor> descriptors, Func<TAttribute, FormattableString> selector)
            where TAttribute : Attribute
        {
            descriptors.Add(new ErrorDetailFilterDescriptor(typeof(ErrorMessageFromAnnotationFilter<TAttribute>), selector));
            return descriptors;
        }
    }
}
