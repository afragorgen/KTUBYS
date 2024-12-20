namespace KTUBYS.wwwroot_js
{
    public class script
    {
        // Sayfa yüklendiğinde çalışacak temel bir fonksiyon
        $(document).ready(function() {
            console.log("Page loaded successfully!");

            // Form doğrulama
            $('#createForm').submit(function (event) {
                var firstName = $('#FirstName').val();
                var lastName = $('#LastName').val();
                var enrollmentDate = $('#EnrollmentDate').val();

                // Eğer formda boş alan varsa, uyarı verir ve form gönderilmez
                if (!firstName || !lastName || !enrollmentDate) {
                    alert("Please fill in all fields.");
                    event.preventDefault(); // Form gönderimini engeller
                }
            });

            // Silme butonuna tıklanırken onay penceresi
            $('.delete-btn').click(function (event) {
                var confirmation = confirm("Are you sure you want to delete this record?");
                if (!confirmation) {
                    event.preventDefault(); // Silme işlemi engellenir
                }
            });

            // AJAX ile veri yükleme
            $('#loadCoursesBtn').click(function () {
                $.ajax({
                    url: '/Courses/GetCourses', // API endpoint
                    type: 'GET',
                    success: function (data) {
                        $('#courseList').empty(); // Mevcut listeyi temizle
                        $.each(data, function (index, course) {
                            $('#courseList').append('<li>' + course.CourseName + '</li>');
                        });
                    },
                    error: function (error) {
                        alert("Error fetching courses.");
                    }
                });
            });

            // Dinamik form içerik değişimi
            $('#advisorSelect').change(function () {
                var selectedAdvisor = $(this).val();
                if (selectedAdvisor === '1') {
                    $('#additionalInfo').show(); // Ekstra bilgi alanını göster
                } else {
                    $('#additionalInfo').hide(); // Ekstra bilgi alanını gizle
                }
            });

            // Tabloyu sıralama
            $('#sortTable').click(function () {
                var rows = $('#studentsTable tbody tr').get();
                rows.sort(function (a, b) {
                    var keyA = $(a).children('td').eq(1).text(); // 2. sütun (FirstName)
                    var keyB = $(b).children('td').eq(1).text();
                    return (keyA < keyB) ? -1 : (keyA > keyB) ? 1 : 0;
                });
                $.each(rows, function (index, row) {
                    $('#studentsTable').children('tbody').append(row);
                });
            });

            // Yazılım güncellemesi bilgisi
            var lastUpdate = new Date('2024-12-01');
            var currentDate = new Date();
            var diff = currentDate - lastUpdate;
            var days = Math.floor(diff / (1000 * 3600 * 24)); // Gün cinsinden fark

            if (days > 7) {
                $('#softwareUpdateInfo').show();
                $('#softwareUpdateInfo').text("A new update is available!");
            }
        });

    }
}
