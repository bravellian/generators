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

namespace Bravellian.Generators.SqlGen.Pipeline._2_SchemaRefinement.Model;

using System.Collections.Generic;

/// <summary>
/// Represents a database index definition.
/// </summary>
public class IndexDefinition
{
    /// <summary>
    /// Gets the name of the index.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a value indicating whether this index is unique.
    /// </summary>
    public bool IsUnique { get; }

    /// <summary>
    /// Gets a value indicating whether this index is clustered.
    /// </summary>
    public bool IsClustered { get; }

    /// <summary>
    /// Gets the column names included in this index.
    /// </summary>
    public List<string> ColumnNames { get; } =[];

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexDefinition"/> class.
    /// Creates a new index definition.
    /// </summary>
    /// <param name="name">The name of the index.</param>
    /// <param name="isUnique">Whether this index is unique.</param>
    /// <param name="isClustered">Whether this index is clustered.</param>
    public IndexDefinition(string name, bool isUnique, bool isClustered)
    {
        this.Name = name;
        this.IsUnique = isUnique;
        this.IsClustered = isClustered;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexDefinition"/> class.
    /// Creates a new index definition.
    /// </summary>
    /// <param name="name">The name of the index.</param>
    /// <param name="isUnique">Whether this index is unique.</param>
    /// <param name="isClustered">Whether this index is clustered.</param>
    /// <param name="columnNames">The column names included in this index.</param>
    public IndexDefinition(string name, bool isUnique, bool isClustered, IEnumerable<string> columnNames)
        : this(name, isUnique, isClustered)
    {
        this.ColumnNames.AddRange(columnNames);
    }
}
