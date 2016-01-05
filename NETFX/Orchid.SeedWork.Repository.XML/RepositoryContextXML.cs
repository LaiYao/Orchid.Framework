using Orchid.SeedWork.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Orchid.SeedWork.Repository.XML
{
    public class RepositoryContextXML : RepositoryContextBase
    {
        #region | Fields |



        #endregion

        #region | Properties |

        #region | XElementRoot |

        public XElement XElementRoot
        {
            get;
            private set;
        }

        #endregion

        #region | XmlFilePath |

        public string XmlFilePath { get; set; }

        #endregion

        #endregion

        #region | Ctor |

        public RepositoryContextXML(string xmlFilePath)
        {
            ExceptionUtilities.ArgumentsChecker(!string.IsNullOrWhiteSpace(xmlFilePath), "xmlFilePath");

            if (!File.Exists(xmlFilePath))
            {
                XmlDocument document = new XmlDocument();
                document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
                document.AppendChild(document.CreateElement("Root"));
                document.Save(xmlFilePath);
            }

            try
            {
                XElementRoot = XElement.Load(xmlFilePath);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }

        #endregion

        #region | Overrides |

        public override void RegisterNew<T>(T value)
        {
            base.RegisterNew<T>(value);
        }

        public override void RegisterModified<T>(T value)
        {
            base.RegisterModified<T>(value);
        }

        public override void Commit()
        {
            XElementRoot.Save(XmlFilePath);
        }

        #endregion

        #region | Methods |



        #endregion
    }
}
