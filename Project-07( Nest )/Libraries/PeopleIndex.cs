using Nest;
using System;

namespace Libraries {
    public class PeopleIndex : ElasticIndex {
        public PeopleIndex(string indexName, string connectionAddress) : base(indexName, connectionAddress) { }

        public PeopleIndex(string indexName, IElasticClient elasticClient) : base(indexName, elasticClient) { }

        public override void CreateIndex() {
            var response = elasticClient.Indices.Create(
                IndexName,
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
            var validator = new ElasticResponseValidator(response);
            if (!validator.IsValid) {
                throw new Exception("Create index failed: \n" + validator.DebugInformation);
            }
        }

        public void search() {
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Match(match => match.
            //            Field("name").
            //            Query("mohammad").
            //            Fuzziness(Fuzziness.EditDistance(1))
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Term(term => term.
            //            Field("name").
            //            Value("mohammad")
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Terms(terms => terms.
            //            Field("name").
            //            Terms("mohammad", "mohamad")
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Range(range => range.
            //            Field("age").
            //            GreaterThanOrEquals(24).
            //            LessThanOrEquals(35)
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        MultiMatch(multiMatch => multiMatch.
            //            Fields(fields=>fields.Field("name").Field("last_name")).
            //            Query("mohammad").
            //            Fuzziness(Fuzziness.EditDistance(1))
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Bool(boolQ=>boolQ.
            //            Must(must=>must.
            //                Match(match=>match.
            //                    Field("name").
            //                    Query("mohammad").
            //                    Fuzziness(Fuzziness.EditDistance(1))
            //                )
            //            ).
            //            Should(should=>should.
            //                Range(range=>range.
            //                    Field("age").
            //                    GreaterThanOrEquals(10).
            //                    LessThanOrEquals(30)
            //                )
            //            )
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Bool(boolQ => boolQ.
            //            Must(must => must.
            //                Match(match => match.
            //                    Field("name").
            //                    Query("mohammad").
            //                    Fuzziness(Fuzziness.EditDistance(1))
            //                )
            //            ).
            //            MustNot(mustNot => mustNot.
            //                Match(match => match.
            //                    Field("last_name").
            //                    Query("mostmand")
            //                )
            //            )
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        GeoDistance(geoDistance => geoDistance.
            //            Field("location").
            //            Location(-34, -22).
            //            Distance(1000, DistanceUnit.Meters)
            //        )
            //    )
            //);
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Query(query => query.
            //        Bool(boolQ => boolQ.
            //            Must(must => must.
            //                Match(
            //                match => match.
            //                    Field("name").
            //                    Query("mohammad").
            //                    Fuzziness(Fuzziness.EditDistance(1))
            //                ),
            //                must => must.Match(match => match.
            //                     Field("last_name").
            //                     Query("mostmand")
            //                )
            //            )
            //        )
            //    )
            //);
            //var documents = response.Documents;
            //var response = elasticClient.Search<Dictionary<string, object>>(s => s.
            //    Index(IndexName).
            //    Aggregations(aggs => aggs.
            //        Sum("docs_age_sum", sum => sum.
            //             Field("age")
            //        )
            //    )
            //);
            return;
        }
    }
}
