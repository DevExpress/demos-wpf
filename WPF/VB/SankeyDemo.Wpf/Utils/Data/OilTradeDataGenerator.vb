Imports System.Collections.Generic

Namespace SankeyDemo

    Public Class OilTradeDataGenerator

        Public Shared Function GetData() As List(Of Export)
            'new Export("Mexico", "United States", 29.9),
            'new Export("Mexico", "Europe", 10.5),
            'new Export("Mexico", "India", 10.0),
            'new Export("South and Central America", "China", 67.2),
            'new Export("Europe", "China", 13.6),
            'new Export("Russia", "United States", 6.6),
            'new Export("Russia", "Europe", 153.0),
            'new Export("Russia", "CIS", 18.4),
            'new Export("Russia", "Middle East", 5.6),
            'new Export("Russia", "China", 77.7),
            'new Export("Russia", "Japan", 7.9),
            'new Export("Russia", "Asia Pacific", 10.4),
            'new Export("CIS", "Europe", 68.1),
            'new Export("CIS", "Asia Pacific", 6.9),
            'new Export("Iraq", "China", 51.8),
            'new Export("Kuwait", "China", 22.7),
            'new Export("Kuwait", "India", 10.6),
            'new Export("Kuwait", "Japan", 12.6),
            'new Export("Kuwait", "Asia Pacific", 39.8),
            'new Export("Saudi Arabia", "Africa", 8.4),
            'new Export("Saudi Arabia", "China", 83.3),
            'new Export("Saudi Arabia", "Singapore", 6.1),
            'new Export("United Arab Emirates", "Australasia", 6.1),
            'new Export("United Arab Emirates", "China", 15.3),
            'new Export("United Arab Emirates", "Singapore", 13.2),
            'new Export(/**/"Middle East", "Middle East", 5.7),
            'new Export(/**/"Middle East", "China", 52.2),
            'new Export(/**/"Middle East", "Singapore", 9.2),
            'new Export("North Africa", "China", 10.7),
            'new Export("West Africa", "Africa", 10.0),
            'new Export("West Africa", "China", 77.8),
            'new Export("Australasia", "Asia Pacific", 6.8),
            'new Export("Asia Pacific", "Australasia", 10.6),
            'new Export("Asia Pacific", "China", 15.6),
            Return New List(Of Export)() From {New Export("Canada", "United States", 189.7), New Export("United States", "Canada", 24.2), New Export("United States", "Europe", 45.8), New Export("United States", "India", 9.1), New Export("South and Central America", "India", 18.7), New Export("Europe", "United States", 6.3), New Export("Iraq", "Europe", 55.4), New Export("Iraq", "United States", 16.5), New Export("Iraq", "India", 49.2), New Export("Iraq", "Asia Pacific", 18.9), New Export("Saudi Arabia", "Canada", 5.1), New Export("Saudi Arabia", "United States", 24.9), New Export("Saudi Arabia", "Europe", 39.9), New Export("Saudi Arabia", "Middle East", 13.2), New Export("Saudi Arabia", "India", 42.6), New Export("Saudi Arabia", "Japan", 52.6), New Export("Saudi Arabia", "Asia Pacific", 78.5), New Export("United Arab Emirates", "India", 19.6), New Export("United Arab Emirates", "Japan", 42.9), New Export("United Arab Emirates", "Asia Pacific", 39.4), New Export("Middle East", "India", 10.8), New Export("Middle East", "Japan", 19.3), New Export("Middle East", "Asia Pacific", 23.5), New Export("North Africa", "Europe", 59.1), New Export("North Africa", "India", 5.6), New Export("North Africa", "Asia Pacific", 5.1), New Export("West Africa", "United States", 14.1), New Export("West Africa", "Europe", 65.1), New Export("West Africa", "India", 30.2), New Export("West Africa", "Asia Pacific", 10.0)}
        End Function
    End Class
End Namespace
