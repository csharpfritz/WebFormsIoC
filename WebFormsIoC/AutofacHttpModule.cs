using Autofac;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace WebFormsIoC
{
  public class AutofacHttpModule : IHttpModule
  {

    private static readonly IContainer Container;

    static AutofacHttpModule()
    {
      var builder = RegisterConfiguration();
      Container = builder.Build();
    }

    private static ContainerBuilder RegisterConfiguration()
    {

      var builder = new ContainerBuilder();

      // Configure autofac
      builder.RegisterAssemblyModules(typeof(AutofacHttpModule).Assembly);
      builder.RegisterAssemblyTypes(typeof(AutofacHttpModule).Assembly);

      return builder;

    }

    public void Init(HttpApplication context)
    {
      context.PreRequestHandlerExecute += Context_PreRequestHandlerExecute;
    }

    private void Context_PreRequestHandlerExecute(object sender, EventArgs e)
    {

      var page = HttpContext.Current.CurrentHandler as Page;

      if (page == null) return;

      InjectDependencies(page);
      Container.InjectProperties(page);

    }

    private void InjectDependencies(Page page)
    {

      var ctor = GetInjectableConstructor(page.GetType().BaseType);

      if (ctor != null)
      {

        var args = (from parm in ctor.GetParameters()
                    select Container.Resolve(parm.ParameterType))
                   .ToArray();

        ctor.Invoke(page, args);

      }

    }

    private static ConstructorInfo GetInjectableConstructor(Type type)
    {

      var possibleConstructors = (
        from ctor in type.GetConstructors()
        where ctor.GetParameters().Length > 0
        select ctor).ToArray();

      if (possibleConstructors.Length == 0) return null;

      if (possibleConstructors.Length == 1) return possibleConstructors[0];

      throw new Exception("Unable to determine which constructor to inject");

    }

    public void Dispose() { }

  }
}