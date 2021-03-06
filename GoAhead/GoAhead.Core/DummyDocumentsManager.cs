﻿using GoAhead.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoAhead.Core
{
    public class DummyDocumentsManager : IDocumentsManager
    {
        private static Dictionary<string, string> Documents { get; set; }

        private static Dictionary<string, string[]> References { get; set; }

        static DummyDocumentsManager()
        {
            Documents = new Dictionary<string, string>()
            {
                {"user_1", "{\"orders\":[1,2,3]}"},
                {"order_1", "{\"orders\":[1,2,3]}"},
                {"order_2", "{\"orders\":[1,2,3]}"},
                {"order_3", "{\"orders\":[1,2,3]}"},
            };

            References = new Dictionary<string, string[]>
            {
                {"user_1", new string[] {"order_1", "order_2", "order_3"}},
                {"order_1", new string[] {"user_1"}},
                {"order_2", new string[] {"user_1"}},
                {"order_3", new string[] {"user_1"}},
            };
        }

        public Document GetDocument(GetDocument documentInfo)
        {
            return new Document()
            {
                Id = documentInfo.Id,
                RawJsonValue = Documents.ContainsKey(documentInfo.Id) ? Documents[documentInfo.Id] : null,
            };
        }

        public IEnumerable<Reference> GetReferences(GetReferences referencesInfo)
        {
            return !References.ContainsKey(referencesInfo.ForDocumentId) ?
                Enumerable.Empty<Reference>() :
                (References[referencesInfo.ForDocumentId] ?? new string[] { })
                    .Select(r => new Reference() { Document = r, Id = "/api/documents/" + r });

        }
    }
}
