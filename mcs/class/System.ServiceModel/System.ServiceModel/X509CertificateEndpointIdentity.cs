//
// X509CertificateEndpointIdentity.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2006 Novell, Inc.  http://www.novell.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;


namespace System.ServiceModel
{
	public class X509CertificateEndpointIdentity : EndpointIdentity
	{
		X509Certificate2 primary;
		X509Certificate2Collection supporting, all;

		public X509CertificateEndpointIdentity (X509Certificate2 cert)
		{
			if (cert == null)
				throw new ArgumentNullException ("cert");
			primary = cert;
			Initialize (Claim.CreateThumbprintClaim (cert.GetCertHash ()));
		}

		public X509CertificateEndpointIdentity (
			X509Certificate2 primaryCertificate,
			X509Certificate2Collection supportingCertificates)
		{
			if (primaryCertificate == null)
				throw new ArgumentNullException ("primaryCertificate");
			if (supportingCertificates == null)
				throw new ArgumentNullException ("supportingCertificates");

			primary = primaryCertificate;
			supporting = supportingCertificates;
		}

		public X509Certificate2Collection Certificates {
			get {
				if (all == null) {
					all = new X509Certificate2Collection ();
					all.Add (primary);
					if (supporting != null)
						all.AddRange (supporting);
				}
				return all;
			}
		}
	}
}
