using Nest;
using System;

namespace Libraries {
    public class PeopleIndex : ElasticIndex {
        public PeopleIndex(string indexName, string connectionAddress) : base(indexName, connectionAddress) { }

        public PeopleIndex(string indexName, IElasticClient elasticClient) : base(indexName, elasticClient) { }

        public override void CreateIndex() {
            var index = IndexName;
            var response = elasticClient.Indices.Create(
                index,
                i => i.Settings(
                    settings => settings.Analysis(analysis => analysis.
                        TokenFilters(tokenFilters => tokenFilters.
                            Length("ten_digits_min", tDM => tDM.Min(10))
                        ).
                        CharFilters(charFilters => charFilters.
                            PatternReplace("digits_only", dO => dO.Pattern(@"[^\d]"))
                        ).
                        Analyzers(analyzer => analyzer.
                            Custom("phone_number", pN => pN.CharFilters("digits_only").Tokenizer("keyword").Filters("ten_digits_min"))
                        )
                    )
                ).
                Map<Person>(map => map.Properties(properties => properties.
                      Number(number => number.Name(person => person.Age)).
                      Text(text => text.Name(person => person.EyeColor)).
                      Text(text => text.Name(person => person.Name).Fields(fields => fields.
                              Keyword(keyword => keyword.Name("keyword").IgnoreAbove(128))
                      )).
                      Boolean(boolean => boolean.Name(person => person.PersonGender)).
                      Text(text => text.Name(person => person.Company).Fields(fields => fields.
                              Keyword(keyword => keyword.Name("keyword").IgnoreAbove(128))
                      )).
                      Keyword(keyword => keyword.Name(person => person.Email)).
                      Text(text => text.Name(person => person.Phone).Analyzer("phone_number")).
                      Text(text => text.Name(person => person.Address)).
                      Text(text => text.Name(person => person.About)).
                      Date(date => date.Name(person => person.RegistrationDate).Format("yyyy/MM/dd HH:mm:ss||yyyy/MM/dd||epoch_millis")).
                      GeoPoint(geoPoint => geoPoint.Name(person => person.Location))
                ))
            );
            if (!response.IsValid) {
                throw new Exception("Create index failed: \n" + response.DebugInformation);
            }
        }

    }
}
