#region Copyright
//  Copyright 2016 OSIsoft, LLC
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
#endregion

using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using System.ServiceProcess;

namespace AFSDK_Windows_Service
{
    public partial class Service1 : ServiceBase
    {
        PIServers servers;
        PIServer server;
        PIPoint target;
        PISystems systems;
        PISystem system;
        AFElement element;
        AFDatabase db;
        AFAttribute attribute;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            servers = new PIServers();
            server = servers.DefaultPIServer;
            target = PIPoint.FindPIPoint(server, "serviceTarget");

            systems = new PISystems();
            system = systems.DefaultPISystem;
            db = system.Databases["696318"];
            element = db.Elements["696318"];
            attribute = element.Attributes["696318"];

            target.UpdateValue(new AFValue("Service A has started"), AFUpdateOption.Insert);
            attribute.Data.UpdateValue(new AFValue("Service A has started"), AFUpdateOption.Insert);
        }

        protected override void OnStop()
        {
            target.UpdateValue(new AFValue("Service A has stopped"), AFUpdateOption.Insert);
            attribute.Data.UpdateValue(new AFValue("Service A has stopped"), AFUpdateOption.Insert);
        }
    }
}