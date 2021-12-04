from rest_framework import viewsets
from learning.models import Test
from learning.services.serializers import TestSerializer
from rest_framework.permissions import AllowAny


class TestViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = Test.objects.all()
    serializer_class = TestSerializer
    permission_classes = [AllowAny]
