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

namespace Bravellian.Generators.SqlGen.Pipeline
{
    using System.Collections.Generic;
    using Bravellian.Generators.SqlGen.Common.Configuration;
    using Bravellian.Generators.SqlGen.Pipeline._1_Ingestion;
    using Bravellian.Generators.SqlGen.Pipeline._1_Ingestion.Model;
    using Bravellian.Generators.SqlGen.Pipeline._2_SchemaRefinement;
    using Bravellian.Generators.SqlGen.Pipeline._3_CSharpTransformation;
    using Bravellian.Generators.SqlGen.Pipeline._4_CodeGeneration;

    public class SqlGenOrchestrator
    {
        private readonly ISchemaIngestor schemaIngestor;
        private readonly ISchemaRefiner schemaRefiner;
        private readonly ICSharpModelTransformer cSharpModelTransformer;
        private readonly ICSharpCodeGenerator cSharpCodeGenerator;
        private readonly IBvLogger logger;
        private readonly SqlConfiguration configuration;

        public SqlGenOrchestrator(
            ISchemaIngestor schemaIngestor,
            ISchemaRefiner schemaRefiner,
            ICSharpModelTransformer cSharpModelTransformer,
            ICSharpCodeGenerator cSharpCodeGenerator,
            SqlConfiguration configuration,
            IBvLogger logger)
        {
            this.schemaIngestor = schemaIngestor;
            this.schemaRefiner = schemaRefiner;
            this.cSharpModelTransformer = cSharpModelTransformer;
            this.cSharpCodeGenerator = cSharpCodeGenerator;
            this.configuration = configuration;
            this.logger = logger;
        }

        public IReadOnlyDictionary<string, string> Generate(string[] sqlFiles)
        {
            this.logger.LogMessage("Phase 1: Ingesting SQL schema...");
            var rawDatabaseModel = this.schemaIngestor.Ingest(sqlFiles);

            this.logger.LogMessage("Phase 2: Refining schema model...");
            var refinedSchema = this.schemaRefiner.Refine(rawDatabaseModel);

            this.logger.LogMessage("Phase 3: Transforming to C# model...");
            var csharpModel = this.cSharpModelTransformer.Transform(refinedSchema);

            this.logger.LogMessage("Phase 4: Generating C# code...");
            var generatedCode = this.cSharpCodeGenerator.Generate(csharpModel);

            this.logger.LogMessage("Code generation complete.");
            return generatedCode;
        }
    }
}
