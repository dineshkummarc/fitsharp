﻿// Copyright © 2012 Syterra Software Inc. All rights reserved.
// The use and distribution terms for this software are covered by the Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file license.txt at the root of this distribution. By using this software in any fashion, you are agreeing
// to be bound by the terms of this license. You must not remove this notice, or any other, from this software.

using System;
using System.Globalization;
using fitSharp.Machine.Engine;
using fitSharp.Machine.Model;

namespace fitSharp.Slim.Operators {
    public class ParseDefault: SlimOperator, ParseOperator<string> {
        public bool CanParse(Type type, TypedValue instance, Tree<string> parameters) {
            return true;
        }

        public TypedValue Parse(Type type, TypedValue instance, Tree<string> parameters) {
            foreach (var parse in new RuntimeType(type).FindStatic(MemberName.ParseMethod, new[] {typeof (string), typeof(IFormatProvider)}).Value) {
                if (parse.ReturnType == type) {
                    return parse.Invoke(new object[] {parameters.Value, CultureInfo.InvariantCulture});
                }
            }
            return new BasicProcessor().Parse(type, instance, parameters);
        }
    }
}
