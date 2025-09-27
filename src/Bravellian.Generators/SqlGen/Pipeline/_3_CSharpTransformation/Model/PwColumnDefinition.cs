// Copyright (c) Bravellian
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#nullable enable

namespace Bravellian.Generators.SqlGen.Pipeline;

using System.Collections.Generic;

/// <summary>
/// Represents a SQL column definition.
/// </summary>
public class PwColumnDefinition(
    string name,
    string csharpType,
    bool isNullable,
    bool isPrimaryKey,
    List<string>? parameters = null,
    string? originalSqlType = null)
{
    public string Name { get; } = name;

    public string CSharpType { get; } = csharpType;

    public bool IsNullable { get; } = isNullable;

    public bool IsPrimaryKey { get; } = isPrimaryKey;

    public List<string>? Parameters { get; } = parameters;

    /// <summary>
    /// Gets the original SQL type of the column (e.g., VARCHAR, NVARCHAR).
    /// </summary>
    public string? OriginalSqlType { get; } = originalSqlType;
}
