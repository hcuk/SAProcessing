/*
    Copyright 2016 Healthcare Communications UK Ltd
 
    This file is part of HCSentimentAnalysisProcessor.

    HCSentimentAnalysisProcessor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HCSentimentAnalysisProcessor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HCSentimentAnalysisProcessor.  If not, see <http://www.gnu.org/licenses/>.

 */

using SAP.Interfaces;
using Microsoft.Practices.Unity;

namespace SAP.Process
{
    /// <summary>
    /// sets up IoC with Unity
    /// </summary>
    public class Factory
    {
        private static readonly Factory _current = new Factory();
        private static readonly UnityContainer _unity = new UnityContainer();

    private Factory()
    {
    }

    public static Factory Current => Factory._current;

    public void RegisterInterfaces()
    {
      Factory._unity.RegisterType<IEngine, SAP.Process.Engine>();
      Factory._unity.RegisterType<IAnalyser, Analyser>();
    }

    public IEngine Engine() => Factory._unity.Resolve<IEngine>();

    public IAnalyser Analyse() => Factory._unity.Resolve<IAnalyser>();
  }
}
