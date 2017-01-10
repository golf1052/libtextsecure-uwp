﻿/** 
 * Copyright (C) 2017 smndtrl, golf1052
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using libsignal.ecc;
using libtextsecure.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace libtextsecure.push
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PreKeyEntity
    {

        [JsonProperty("keyId")]
        private uint keyId;

        [JsonProperty("publicKey")]
        [JsonConverter(typeof(ECPublicKeySerializer))]
        private ECPublicKey publicKey;

        public PreKeyEntity() { }

        internal PreKeyEntity(uint keyId, ECPublicKey publicKey)
        {
            this.keyId = keyId;
            this.publicKey = publicKey;
        }

        public uint getKeyId()
        {
            return keyId;
        }

        public ECPublicKey getPublicKey()
        {
            return publicKey;
        }

        private class ECPublicKeySerializer : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                try
                {
                    var token = JToken.Load(reader);

                    string key = token.Value<string>();
                    //ECPublicKey pubKey = (ECPublicKey)existingValue;
                    return Curve.decodePoint(Base64.decodeWithoutPadding(key), 0);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                ECPublicKey pubKey = (ECPublicKey)value;

                writer.WriteValue(Base64.encodeBytesWithoutPadding(pubKey.serialize()));
            }
        }
    }
}
