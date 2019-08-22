Imports WIA
Imports System.Xml

Module PropertiesToXML

    Sub PropertiesToXML(properties As WIA.Properties, writer As XmlTextWriter)
        For Each prop As WIA.Property In properties

            writer.WriteStartElement("Property")
            Try

                writer.WriteAttributeString("ID", prop.PropertyID)
                writer.WriteAttributeString("Name", prop.Name)
                writer.WriteAttributeString("IsReadOnly", prop.IsReadOnly)
                writer.WriteAttributeString("Type", String.Format("{0}", DirectCast(prop.Type, WiaPropertyType)))
                writer.WriteAttributeString("Value", prop.Value.ToString())

                writer.WriteStartElement("Subtype")
                writer.WriteAttributeString("Type", String.Format("{0}", prop.SubType))
                Select Case prop.SubType
                    Case WiaSubType.UnspecifiedSubType
                        writer.WriteAttributeString("Count", 0)
                    Case WiaSubType.FlagSubType
                        writer.WriteAttributeString("Count", String.Format("{0}", prop.SubTypeValues.Count))
                        For Each o As Object In prop.SubTypeValues
                            writer.WriteStartElement("Flag")
                            writer.WriteString(o.ToString())
                            writer.WriteEndElement()
                        Next
                    Case WiaSubType.ListSubType
                        writer.WriteAttributeString("Count", String.Format("{0}", prop.SubTypeValues.Count))
                        For Each o As Object In prop.SubTypeValues
                            writer.WriteStartElement("Value")
                            writer.WriteString(o.ToString())
                            writer.WriteEndElement()
                        Next
                    Case WiaSubType.RangeSubType
                        writer.WriteAttributeString("Count", 3)
                        writer.WriteElementString("Min", prop.SubTypeMin.ToString())
                        writer.WriteElementString("Max", prop.SubTypeMax.ToString())
                        writer.WriteElementString("Step", prop.SubTypeStep.ToString())
                    Case Else

                End Select
                writer.WriteEndElement()
            Catch ex As Exception
                writer.WriteString("Couldn't evaluate")
            End Try
            writer.WriteEndElement()
        Next

        writer.Flush()
    End Sub
End Module
