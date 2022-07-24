using System;
using System.Collections.Generic;
using System.Text;

namespace RequestResponseModel.Response {
	public class RServiceProvider<T> {
        public bool Success { get; set; } = false;

        public string Message { get; set; }

        public string ReturnCode { get; set; }

        public bool HasResult { get; set; }
        private T _Result = default(T);
        public T Result {
            get
            {
                if (_Result == null) {
                    _Result = System.Activator.CreateInstance<T>();
                }
                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }
    }
}
