// ///////////////////////////////////////////////////////////////////
// This file is a part of EasyFarm for Final Fantasy XI
// Copyright (C) 2013-2017 Mykezero
// 
// EasyFarm is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// EasyFarm is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// If not, see <http://www.gnu.org/licenses/>.
// ///////////////////////////////////////////////////////////////////
using EasyFarm.Classes;
using System.Collections.ObjectModel;
using EasyFarm.UserSettings;

namespace EasyFarm.ViewModels
{
    public class IgnoredViewModel : ListViewModel<string>
    {
        public IgnoredViewModel()
        {
            ViewName = "Ignored";
        }

        public override string Value
        {
            get { return Config.Instance.IgnoredName; }
            set { SetProperty(ref Config.Instance.IgnoredName, value); }
        }

        public override ObservableCollection<string> Values
        {
            get { return Config.Instance.IgnoredMobs; }
            set { SetProperty(ref Config.Instance.IgnoredMobs, value); }
        }

        protected override void Add()
        {
            if (string.IsNullOrWhiteSpace(Value)) return;
            base.Add();
            Value = "";
        }

        protected override void Clear()
        {
            base.Clear();
            Value = "";
        }
    }
}